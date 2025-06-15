import { Component, inject, OnInit } from '@angular/core';
import { LocationService } from '../../../../core/services/location/location.service';

@Component({
  selector: 'app-location-list',
  imports: [],
  templateUrl: './location-list.page.html',
  styleUrl: './location-list.page.scss'
})
export class LocationListPage implements OnInit{
private locationService = inject(LocationService);

  ngOnInit() {
    this.locationService.getAllLocations().subscribe((res) => {
      console.log('Locaciones:', res);
    });
  }
}
