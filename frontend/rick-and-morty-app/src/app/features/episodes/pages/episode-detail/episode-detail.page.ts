import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EpisodeDto } from '../../../../core/services/models/episode.dto';
import { EpisodeService } from '../../../../core/services/episode/episode.service';
import { CharacterService } from '../../../../core/services/character/character.service';

@Component({
  standalone: true,
  selector: 'app-episode-detail',
  imports: [CommonModule],
  templateUrl: './episode-detail.page.html',
  styleUrl: './episode-detail.page.scss'
})
export class EpisodeDetailPage implements OnInit{

  private route = inject(ActivatedRoute);
  private episodeService = inject(EpisodeService);
  private characterService = inject(CharacterService);

  episode? : EpisodeDto;
  selectedCharacterImage: string | null = null;

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if(id){
      this.episodeService.getEpisodeById(+id).subscribe({
        next: (data) => this.episode = data.results[0],
        error: (error) => console.error('Error al obtener el detalle del episodio', error)
      });
    }
  }

  showCharacterImage(url : string): void {
    const id = url.split('/').pop();
    console.log(id);

    if(id){
      this.characterService.getCharacterById(id).subscribe(character => {
        this.selectedCharacterImage = character.results[0].image;
      });
    }
  }
}
