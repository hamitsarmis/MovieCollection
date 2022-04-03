import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Movie} from "./movie.model";
import {Observable, of} from "rxjs";
import {environment} from "./environment";

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private _movies: Movie[] | undefined;

  getMovies() : Observable<Movie[]> {
    if(this._movies !== undefined)
      return  of(this._movies);
    return this.httpService.post<Movie[]>(environment.baseUrl + 'Movie/get-movies', null)
  }
  constructor(private httpService: HttpClient) {  }
}
