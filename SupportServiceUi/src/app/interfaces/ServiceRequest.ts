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