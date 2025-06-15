import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { EpisodeDto } from '../models/episode.dto';
import { EpisodeFilter } from '../models/episode-filter';

@Injectable({
  providedIn: 'root',
})
export class EpisodeService {
  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiBaseUrl;

  getAllEpisodes(
    page: number,
    filters?: EpisodeFilter
  ): Observable<ApiResponse<EpisodeDto>> {
    if (!filters?.hasFilters()) {
      return this.http.get<ApiResponse<EpisodeDto>>(
        `${this.apiUrl}/Episodes/all?page=${page}`
      );
    }

    let params = new HttpParams();
    const query = filters.toQueryParams();

    for (const key in query) {
      params = params.set(key, query[key]);
    }
    return this.http.get<ApiResponse<EpisodeDto>>(
      `${this.apiUrl}/Episodes/filtered`,
      { params }
    );
  }

  getEpisodeById(episodeId : number) : Observable<ApiResponse<EpisodeDto>>{
    return this,this.http.get<ApiResponse<EpisodeDto>>(
      `${this.apiUrl}/Episodes/single?episode=${episodeId}`
    )
  }
}
