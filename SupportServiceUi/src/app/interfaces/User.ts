import { Role } from "./Role";

export interface UserPreview {
    id: number;
    name: string;
    email: string;
    isDeactivated: boolean;
}

export interface EditUser {
    id: number;
    name: string;
    email: string;
    role: Role;
}