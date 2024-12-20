import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProdutotelaComponent } from './produtotela.component';

describe('ProdutotelaComponent', () => {
  let component: ProdutotelaComponent;
  let fixture: ComponentFixture<ProdutotelaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProdutotelaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProdutotelaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
