import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { CharacterDto } from '../models/character.dto';
import { ApiResponse } from '../models/api-response.model';
import { CharacterFilter } from '../models/character-filter';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {

  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiBaseUrl;

 getAllCharacters(): Observable<ApiResponse<CharacterDto>> {
    return this.http.get<ApiResponse<CharacterDto>>(`${this.apiUrl}/Character/all`);
  }

  getFilteredCharacters(filter: CharacterFilter): Observable<ApiResponse<CharacterDto>> {
    return this.http.get<ApiResponse<CharacterDto>>(`${this.apiUrl}/Character/filtered?${filter.toQueryString()}`);
  }

  getCharacterById(id : string): Observable<ApiResponse<CharacterDto>>{
    return this.http.get<ApiResponse<CharacterDto>>(`${this.apiUrl}/Character/single?characterId=${id}`)
  }
}
