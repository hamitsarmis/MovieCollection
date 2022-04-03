import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-movie-detail',
  templateUrl: './movie-detail.component.html',
  styleUrls: ['./movie-detail.component.css']
})
export class MovieDetailComponent implements OnInit {

  @Input() Name = '';
  @Input() ImagePath = '';
  @Input() ImdbScore = '0';
  @Input() Description = '';

  constructor() {  }

  ngOnInit(): void {
  }

}
