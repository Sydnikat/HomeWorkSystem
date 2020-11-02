import express, { Request, Response } from "express";
import bcrypt from "bcrypt";
import jwt, { Secret } from "jsonwebtoken";
import { v4 as uuidv4 } from "uuid";
import { User } from "../schema/user";

const router = express.Router();

router.post("/signin", async (req: Request, res: Response) => {
  const query = { userName: req.body.userName };
  User.findOne(query, async (err, user) => {
    if (err) {
      console.log(err);
      return res.status(500).send();
    }
    if (user === null) {
      return res.status(401).send();
    } else {
      if (await bcrypt.compare(req.body.password, user.password)) {
        const accessToken = jwt.sign(
          user.toJSON(),
          process.env.ACCESS_TOKEN_SECRET!,
          { expiresIn: "1h" }
        );

        const refreshToken = jwt.sign(
          user.toJSON(),
          process.env.REFRESH_TOKEN_SECRET!,
          { expiresIn: "6h" }
        );

        return res.json({
          id: user.id,
          userName: user.userName,
          userFullName: user.userFullName,
          role: user.role,
          accessToken: accessToken,
          refreshToken: refreshToken,
        });
      } else {
        return res.status(401).send();
      }
    }
  });
});

router.post("/signup", async (req: Request, res: Response) => {
  const query = { userName: req.body.userName };
  User.findOne(query, async (err, user) => {
    if (err) {
      console.log(err);
      return res.status(500).send();
    }

    if (user === null) {
      if (req.body.password !== req.body.repassword) {
        return res.status(400).send();
      } else {
        const salt = await bcrypt.genSalt();
        const hashedPassword = await bcrypt.hash(req.body.password, salt);

        const newUser = new User({
          id: uuidv4(),
          userName: req.body.userName,
          userFullName: req.body.userFullName,
          password: hashedPassword,
          role: req.body.role,
        });

        newUser.save((err) => {
          if (err) {
            console.log(err);
            return res.status(500).send();
          }

          return res.status(201).send();
        });
      }
    } else {
      return res.status(409).send();
    }
  });
});

router.post("/refresh", async (req: Request, res: Response) => {
  const refreshToken = req.body.refreshToken as string;
  if (refreshToken === null) {
    return res.status(401).send();
  }

  jwt.verify(
    refreshToken,
    process.env.REFRESH_TOKEN_SECRET as Secret,
    (err, user) => {
      if (err) {
        return res.status(401).send();
      }

      const accessToken = jwt.sign({ user }, process.env.ACCESS_TOKEN_SECRET!, {
        expiresIn: "1h",
      });

      return res.json({
        token: accessToken,
      });
    }
  );
});

export default router;
