import { Component, inject, OnInit } from '@angular/core';
import { CharacterDto } from '../../../../core/services/models/character.dto';
import { CharacterService } from '../../../../core/services/character/character.service';
import { CommonModule } from '@angular/common';
import { CharacterFilter } from '../../../../core/services/models/character-filter';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-character-list.page',
  imports: [CommonModule, FormsModule],
  templateUrl: './character-list.page.component.html',
  styleUrl: './character-list.page.component.scss'
})
export class CharacterListPageComponent implements OnInit{

private characterService = inject(CharacterService);

characters: CharacterDto[] = [];
currentPage = 1;
totalPages = 1;
filters : CharacterFilter  = new CharacterFilter(); 

ngOnInit(): void {
  this.loadCharacters();
}

loadCharacters(): void {
  console.log(this.filters)
  this.characterService.getAllCharacters(this.currentPage, this.filters).subscribe(response => {
    this.characters = response.results;
    this.totalPages = response.info.pages;
  });
}

onPageChange(newPage: number): void {
  this.currentPage = newPage;
  this.loadCharacters();
}

applyFilters(): void {
    this.loadCharacters();
  }

  prevPage(): void {
    this.currentPage -= 1;
    this.loadCharacters();
  }
  
  nextPage(): void {
    this.currentPage += 1;
    this.loadCharacters();
  }

  firstPage(): void {
    this.currentPage = 1;
    this.loadCharacters();
  }

  lastPage(): void {
    this.currentPage = this.totalPages;
    this.loadCharacters();
  }
}
