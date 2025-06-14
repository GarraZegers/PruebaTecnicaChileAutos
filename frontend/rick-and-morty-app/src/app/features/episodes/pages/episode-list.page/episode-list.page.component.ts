import { Component, OnInit } from '@angular/core';
import { EpisodeService } from '../../../../core/services/episode.service';
import { EpisodeDto } from '../../../../core/services/models/episode.dto';
import { CommonModule } from '@angular/common';
import { EpisodeFilter } from '../../../../core/services/models/episode-filter';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-episode-list.page',
  imports: [CommonModule, FormsModule],
  templateUrl: './episode-list.page.component.html',
  styleUrl: './episode-list.page.component.scss',
})
export class EpisodeListPageComponent implements OnInit {
  episodes: EpisodeDto[] = [];
  filters: EpisodeFilter = new EpisodeFilter();
  totalPages = 1;
  currentPage = 1;

  constructor(private episodeService: EpisodeService) {}

  ngOnInit(): void {
    this.loadEpisodes();
  }

  loadEpisodes(): void {
    this.episodeService.getAllEpisodes(this.currentPage, this.filters).subscribe({
      next: (res) => {
        this.episodes = res.results;
        this.totalPages = res.info.pages;
      },
      error: (error) => {
        console.error('Error al cargar los episodios', error);
      },
    });
  }

  goToPage(page: number): void {
    if (page < 1 || page > this.totalPages) return;
    this.currentPage = page;
    this.loadEpisodes();
  }

  applyFilters(): void {
    this.loadEpisodes();
  }
}
