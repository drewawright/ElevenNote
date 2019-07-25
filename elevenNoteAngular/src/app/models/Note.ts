export interface Note {
    NoteId?: number;
    OwnerId?: number;
    Title: string;
    Content: string;
    CategoryID: number;
    CategoryName: string;
    IsStarred?: boolean;
    CreatedUtc?: Date;
    ModifiedUtc?: Date;
}