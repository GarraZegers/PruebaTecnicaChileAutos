import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EpisodeDetailPage } from './episode-detail.page';

describe('EpisodeDetailPage', () => {
  let component: EpisodeDetailPage;
  let fixture: ComponentFixture<EpisodeDetailPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EpisodeDetailPage]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EpisodeDetailPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
