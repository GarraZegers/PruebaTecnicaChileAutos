import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { CharacterDto } from '../models/character.dto';
import { ApiResponse } from '../models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class CharacterService {

  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiBaseUrl;

  getCharacterById(id : string): Observable<ApiResponse<CharacterDto>>{
    return this.http.get<ApiResponse<CharacterDto>>(`${this.apiUrl}/Character/single?characterId=${id}`)
  }
}
