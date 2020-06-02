export class Users {
  id: number;
  avatar: File;
  avatarFileName: string;
  username: string;
  firstname: string;
  lastname: string;
  phone: number;
  email: string;
  password: string;
  authority: number;
  endedWellCount: number;
}
export class Recomendation {
  id: number;
  addedById: number;
  addedBy: Users;
  content: string;
  forUserId: number;
  forUser:Users;
}
