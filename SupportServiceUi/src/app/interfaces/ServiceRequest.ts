import { Group } from "./Group"
import { Status } from "./Status"
import { UserPreview } from "./User"

export interface ServiceRequestPreview {
    id: number,
    title: string,
    status: string
}

export interface ServiceRequestOverview {
    id: number,
    title: string,
    description: string,
    status: string,
    createdDate: Date,
    updatedDate: Date
    createdBy: string,
    appointed: string
}

export interface EditServiceRequest {
    id: number,
    title: string,
    description: string,
    status: Status,
    appointed: UserPreview | null,
    group: Group | null
}

export interface CreateServiceRequestDto {
    title: string,
    description: string,
    groupId: number | null,
    createdById: number,
    appointedId: number | null
}