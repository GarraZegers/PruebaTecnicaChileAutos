import { Component, inject, OnInit } from '@angular/core';
import { LocationService } from '../../../../core/services/location/location.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { LocationDto } from '../../../../core/services/models/location.dto';
import { LocationFilter } from '../../../../core/services/models/location-filter';

@Component({
  standalone: true,
  selector: 'app-location-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './location-list.page.html',
  styleUrl: './location-list.page.scss',
})
export class LocationListPage implements OnInit {
  private locationService = inject(LocationService);

  locations: LocationDto[] = [];
  filters: LocationFilter = new LocationFilter();
  currentPage = 1;
  totalPages = 0;

  ngOnInit() {
    this.loadLocations();
  }

  loadLocations() {
    this.locationService.getAllLocations( this.currentPage, this.filters).subscribe(response => {
      this.locations = response.results;
      this.totalPages = response.info.pages;
    });
  }

  prevPage(): void {
    this.currentPage -= 1;
    this.loadLocations();
  }
  
  nextPage(): void {
    this.currentPage += 1;
    this.loadLocations();
  }

  firstPage(): void {
    this.currentPage = 1;
    this.loadLocations();
  }

  lastPage(): void {
    this.currentPage = this.totalPages;
    this.loadLocations();
  }

  applyFilters(): void {
    this.loadLocations();
  }
}
