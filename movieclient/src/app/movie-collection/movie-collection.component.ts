import {Component, Input, OnInit} from '@angular/core';
import {MovieCollection} from "../moviecollection.model";

@Component({
  selector: 'app-movie-collection',
  templateUrl: './movie-collection.component.html',
  styleUrls: ['./movie-collection.component.css']
})
export class MovieCollectionComponent implements OnInit {

  @Input() movieCollection : MovieCollection | undefined;
  constructor() { }

  ngOnInit(): void {
  }

}
