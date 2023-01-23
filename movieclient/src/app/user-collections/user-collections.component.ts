import {Component, OnInit, Input} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {Movie} from "../movie.model";
import {MovieCollection} from "../moviecollection.model";
import {ToastrService} from "ngx-toastr";
import {MovieService} from "../movie.service";
import {AuthService} from "../auth.service";
import {BehaviorSubject} from "rxjs";
import {environment} from "../environment";
import {NgxSpinnerService} from "ngx-spinner";
import Utils from "../utils";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-user-collections',
  templateUrl: './user-collections.component.html',
  styleUrls: ['./user-collections.component.css']
})
export class UserCollectionsComponent implements OnInit {

  private _searchText = '';
  movieCollections: MovieCollection[] = [];
  initialData: MovieCollection[] = [];
  selectedMovies: Movie[] = [];
  allMovies: Movie[] = [];
  noDataFound: boolean = false;
  isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  loggedInUserId: string = '';
  nameControl = new FormControl('');

  get searchText(){
    return this._searchText;
  }

  @Input()
  set searchText(searchText: string){
    this._searchText = searchText;
    if(this._searchText.length == 0)
      this.movieCollections = this.initialData;
    else {
      this.movieCollections = this.initialData.filter(m=> m.name!.toLowerCase().includes(this._searchText.toLowerCase()));
      if(this.movieCollections.filter(m => m.id == this.SelectedCollection?.id).length == 0) {
        this.SelectedCollection = undefined;
      }
    }
  }
  private _selectedCollection: MovieCollection | undefined;

  get SelectedCollection(): MovieCollection | undefined {
    return this._selectedCollection;
  }

  set SelectedCollection(value: MovieCollection | undefined) {
    this.spinner.show();
    this._selectedCollection = value;
    if (this._selectedCollection != null) {
      this.nameControl.setValue(this._selectedCollection.name);
      this.httpClient.post<Movie[]>(
        environment.baseUrl +'MovieCollection/get-moviesofcollection?collectionId='+ value?.id, null).
      subscribe((data) =>{
        this.selectedMovies = data;
      }, (error => this.handleError(error)), ()=>{ this.spinner.hide(); });
    } else {
      this.spinner.hide();
      this.nameControl.setValue('');
    }
  }

  constructor(private httpClient: HttpClient, private toastr: ToastrService,
              private movieService: MovieService, private authService: AuthService,
              private spinner: NgxSpinnerService) {  }

  private getUrlForCollections() : string{
    if(this.isUserLoggedIn.getValue() === true)
      return environment.baseUrl +'MovieCollection/get-collectionsofuser?userid=' +
        this.authService.getUser().userId;
    return environment.baseUrl + 'MovieCollection/get-collections';
  }

  ngOnInit(): void {

    this.httpClient.post<MovieCollection[]>(
      this.getUrlForCollections()
      , null).
    subscribe((data) => {
      this.initialData = this.movieCollections = data;
      this.noDataFound = this.movieCollections.length == 0;
    });
    this.movieService.getMovies().subscribe((data) => this.allMovies = data);
    this.authService.isUserLoggedIn.subscribe((data) => {
      this.isUserLoggedIn.next(data === true);
      if(data === true)
        this.loggedInUserId = this.authService.getUser().userId.toString();
      else
        this.loggedInUserId = '';
    });
  }

  createNewCollection(){
    let user = this.authService.getUser();
    let movieCollection = {
      id:0,
      name: 'New Collection',
      userId: user.userId,
      userName: user.userName
    };
    this.movieCollections.push(movieCollection);
    this.initialData = this.movieCollections;
    this.SelectedCollection = movieCollection;
  }

  isItemSelected(movie: Movie){
    return this.selectedMovies.filter(m => m.id === movie.id).length > 0;
  }

  removeMovieFromCollection(movie: Movie) {
    this.httpClient.post(
      environment.baseUrl +'MovieCollection/removefrom-moviecollection?collectionId=' +
      this._selectedCollection?.id, movie, {headers : new HttpHeaders({ 'Content-Type': 'application/json' })}).
    subscribe((data) => {
      let index = this.selectedMovies.findIndex(m => m.id === movie.id);
      this.selectedMovies.splice(index, 1);
    }, (error => this.handleError(error)));
  }
  addMovieToCollection(movie: Movie){
    this.httpClient.post(
      environment.baseUrl +'MovieCollection/addto-moviecollection?collectionId=' +
      this._selectedCollection?.id, movie).
    subscribe((data) => {
      this.selectedMovies.push(movie);
    }, (error => this.handleError(error)));
  }

  updateCollection(SelectedCollection: MovieCollection) {
    let tempName = SelectedCollection.name;
    SelectedCollection.name = this.nameControl.value;
    this.httpClient.post(
      environment.baseUrl +'MovieCollection/update-moviecollection', SelectedCollection)
      .subscribe((data) =>{
        if(this._selectedCollection !== undefined)
          this._selectedCollection.name = this.nameControl.value;
      }, (error) => {
        SelectedCollection.name = tempName;
        this.handleError(error);
      });
  }

  insertCollection(selectedCollection: MovieCollection) {
    this.httpClient.post<MovieCollection>(
      environment.baseUrl +'MovieCollection/create-moviecollection', selectedCollection)
      .subscribe((data) =>{
        selectedCollection.id = data.id;
      }, (error) => this.handleError(error));
  }

  handleError(error: HttpErrorResponse) {
    this.toastr.error(Utils.getErrorMessage(error),
      'Error', {timeOut: 1000, positionClass: 'toast-bottom-right' });
  }

  deleteCollection(selectedCollection: MovieCollection) {
    if(selectedCollection.id !== 0) {
    this.httpClient.post(
      environment.baseUrl +'MovieCollection/delete-moviecollection', selectedCollection)
      .subscribe((data) =>{
        this.collectionDeleted(selectedCollection);
      }, (error) => this.handleError(error));
    } else
      this.collectionDeleted(selectedCollection);
  }

  collectionDeleted(selectedCollection: MovieCollection){
    let index = this.movieCollections.findIndex(m => m.id === selectedCollection.id);
    this.movieCollections.splice(index, 1);
    this.initialData = this.movieCollections;
    this.SelectedCollection = undefined;
  }
}
