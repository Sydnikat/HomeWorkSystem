import express, { Request, Response } from "express";
import bcrypt from "bcrypt";
import jwt from "jsonwebtoken";
import { v4 as uuidv4 } from "uuid";
import { User } from "../schema/user";

const router = express.Router();

router.post("/signin", async (req: Request, res: Response) => {
  const query = { userName: req.body.userName };
  User.findOne(query, async (err, user) => {
    if (err) {
      console.log(err);
      res.status(500).send();
    }

    if (user === null) {
      res.status(401).send();
    } else {
      if (await bcrypt.compare(req.body.password, user.password)) {
        const accessToken = jwt.sign(
          user.toJSON(),
          process.env.ACCESS_TOKEN_SECRET!,
          { expiresIn: "6h" }
        );
        res.json({
          id: user.id,
          userName: user.userName,
          userFullName: user.userFullName,
          role: user.role,
          accessToken: accessToken,
        });
      } else {
        res.status(401).send();
      }
    }
  });
});

router.post("/signup", async (req: Request, res: Response) => {
  const query = { userName: req.body.userName };
  User.findOne(query, async (err, user) => {
    if (err) {
      console.log(err);
      res.status(500).send();
    }

    if (user === null) {
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
          res.status(500).send();
        } else {
          res.status(201).send();
        }
      });
    } else {
      res.status(409).send();
    }
  });
});

export default router;
