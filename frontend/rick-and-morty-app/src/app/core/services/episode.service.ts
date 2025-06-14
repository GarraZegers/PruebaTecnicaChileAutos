import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { ApiResponse } from './models/api-response.model';
import { EpisodeDto } from './models/episode.dto';

@Injectable({
  providedIn: 'root'
})
export class EpisodeService {
  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiBaseUrl;

  getAllEpisodes(): Observable<ApiResponse<EpisodeDto>>{
    return this.http.get<ApiResponse<EpisodeDto>>(`${this.apiUrl}/Episodes/all`);
  }
}
