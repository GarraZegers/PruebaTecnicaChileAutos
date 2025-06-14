import { Component, OnInit } from '@angular/core';
import { EpisodeService } from '../../../../core/services/episode.service';
import { EpisodeDto } from '../../../../core/services/models/episode.dto';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-episode-list.page',
  imports: [CommonModule],
  templateUrl: './episode-list.page.component.html',
  styleUrl: './episode-list.page.component.scss'
})
export class EpisodeListPageComponent implements OnInit{
  
  episodes: EpisodeDto[] = [];


  constructor(private episodeService: EpisodeService){

  }
  
  ngOnInit(): void {
    this.episodeService.getAllEpisodes().subscribe({
      next: (res) => {
        this.episodes = res.results
      },
      error: (error) => {
        console.error('Error al cargar los episodios', error);
      }
    })
  }

}
