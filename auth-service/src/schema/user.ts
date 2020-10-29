import * as mongoose from "mongoose";
import { IUser } from "../model/user";

export interface UserEntity extends IUser, mongoose.Document {}

let UserSchema = new mongoose.Schema({
  id: String,
  userName: String,
  userFullName: String,
  password: String,
  role: String,
});

export var User = mongoose.model<UserEntity>("User", UserSchema, "users");
