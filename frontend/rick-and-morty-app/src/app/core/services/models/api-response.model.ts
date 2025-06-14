export interface PageInfo{
    count: number;
    pages: number;
    next: string | null;
    prev: string | null;
}

export interface ApiResponse<T> {
    info: PageInfo;
    results: T[];
}