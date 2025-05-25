export interface AuditLog {
    entityId: number,
    propertyName: string,
    oldValue: string,
    newValue: string,
    changedAt: Date,
    changedBy: string
}