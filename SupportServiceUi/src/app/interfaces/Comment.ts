import { AttachmentPreview } from "./Attachment"

export interface Comment {
    id: number,
    message: string,
    name: string,
    email: string,
    createdAt: Date,
    attachments: AttachmentPreview[]
}

export interface CommentToCreate {
    message: string,
    createdById: number,
    serviceRequestId: number
}