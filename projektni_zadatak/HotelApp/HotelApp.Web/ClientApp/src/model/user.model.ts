import { Role } from "./role.model";

export class User {
    id!: number;
    firstName!: string;
    lastName!: string;
    email!: string;
    role!: Role;
    token?: string;
}
