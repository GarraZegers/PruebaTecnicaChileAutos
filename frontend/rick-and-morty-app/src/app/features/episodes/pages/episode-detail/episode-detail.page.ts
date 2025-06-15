import { CommonModule, Location } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EpisodeDto } from '../../../../core/services/models/episode.dto';
import { EpisodeService } from '../../../../core/services/episode/episode.service';
import { CharacterService } from '../../../../core/services/character/character.service';
import { CharacterDto } from '../../../../core/services/models/character.dto';
import { ApiResponse } from '../../../../core/services/models/api-response.model';

@Component({
  standalone: true,
  selector: 'app-episode-detail',
  imports: [CommonModule],
  templateUrl: './episode-detail.page.html',
  styleUrl: './episode-detail.page.scss',
})
export class EpisodeDetailPage implements OnInit {
  private route = inject(ActivatedRoute);
  private episodeService = inject(EpisodeService);
  private characterService = inject(CharacterService);
  private location = inject(Location);


  episode?: EpisodeDto;
  characterObject: CharacterDto | null = null;
  clickedCharacters: string[] = [];

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.episodeService.getEpisodeById(+id).subscribe({
        next: (data) => (this.episode = data.results[0]),
        error: (error) =>
          console.error('Error al obtener el detalle del episodio', error),
      });
    }
  }

  showCharacterImage(url: string): void {
    if (!this.clickedCharacters.includes(url)) {
      this.clickedCharacters.push(url);
    }
    const id = url.split('/').pop();

    if (id) {
      this.characterService.getCharacterById(id).subscribe({
        next: (character: ApiResponse<CharacterDto>) => {
          this.characterObject = character.results[0];
        },
        error: (error) => {
          console.error('Error al cargar la imagen del personaje', error);
        },
      });
    }
  }

  goBack() : void{
    this.location.back();
  }
}
