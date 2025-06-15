import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { LocationDto } from '../models/location.dto';
import { LocationFilter } from '../models/location-filter';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private http = inject(HttpClient);
  private readonly apiUrl = environment.apiBaseUrl;

  getAllLocations(page: number, filters? : LocationFilter): Observable<ApiResponse<LocationDto>> {
    if(!filters?.hasFilters()){
      return this.http.get<ApiResponse<LocationDto>>(`${this.apiUrl}/Location/all?page=${page}`);
    }
    let params = new HttpParams();
        const query = filters.toQueryString();

        return this.http.get<ApiResponse<LocationDto>>(
          `${this.apiUrl}/Location/filtered${query ? '?' + query : ''}`,
          { params }
        );
    
  }

  getFilteredLocations(filter: LocationFilter): Observable<ApiResponse<LocationDto>> {
    return this.http.get<ApiResponse<LocationDto>>(`${this.apiUrl}/Location/filtered?${filter.toQueryString()}`);
  }

  getLocationById(id: number): Observable<LocationDto> {
    return this.http.get<LocationDto>(`${this.apiUrl}/Location/${id}`);
  }
}
