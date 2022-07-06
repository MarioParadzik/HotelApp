import { Observable } from "rxjs";

export class PagedList<TRecord> {
    currentPage!: number;
    totalPages!: number;
    pageSize!: number;
    totalCount!: number;
    hasPrevious!: boolean;
    hasNext!: boolean;
    records!: TRecord[];
  }
  