import {HttpErrorResponse} from "@angular/common/http";

export default class {
  static getErrorMessage(error: HttpErrorResponse){
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}\nError: ${error.error}`;
    }
    return errorMessage;
  }
}
