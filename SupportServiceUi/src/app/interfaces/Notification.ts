export interface Notification {
    id: number,
    title: string,
    message: string,
    isRead: boolean,
    createdAt: Date
}

export interface CreateNotification {
    title: string,
    message: string,
    userId: number
}
