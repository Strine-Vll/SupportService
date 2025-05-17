export interface Stat {
    id: number;
    satisfactionIndex: number;
    reescalateAmount: number;
    reactionTime: Date | null;
    resolutionTime: Date | null;
}

export interface StatFilter {
    startDate: Date | null;
    endDate: Date | null;
    userId: number | null;
}