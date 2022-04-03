import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Movie} from "../movie.model";
import {MovieService} from "../movie.service";
import {AuthService} from "../auth.service";

@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css']
})
export class MovieListComponent implements OnInit {

  private _searchText = '';
  private initialData: Movie[] = []
  @Input() movies: Movie[] = [];

  get searchText(){
    return this._searchText;
  }

  @Input()
  set searchText(searchText: string){
    this._searchText = searchText;
    if(this._searchText.length == 0)
      this.movies = this.initialData;
    else
      this.movies = this.initialData.filter(m=> m.name.toLowerCase().startsWith(this._searchText.toLowerCase()));
  }

  constructor(private movieService:MovieService, private authService: AuthService) { }

  ngOnInit(): void {
    this.movieService.getMovies()
      .subscribe((data) => {
        this.initialData = this.movies = data;
      });
  }

}
