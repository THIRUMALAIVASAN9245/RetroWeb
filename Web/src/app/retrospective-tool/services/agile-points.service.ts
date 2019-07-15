import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { environment } from '../../../environments/environment.prod';
import { AgilePointDetails } from '../models/agile-point-details';

/**
 * @name HttpAgilePointService
 * @description 
 * * this service to handle get list of Agile Point
*/
@Injectable()
export class HttpAgilePointService {

    /**
     * @type {string}
    */
    private BASE_URL: string = environment.apiUrl + '/AgilePoint/';

    /**
     * Constructor for HttpAgilePointService class
     * @param http
     * @param errorService
    */
    constructor(private http: Http) { }

    /**
     * @name addRetroPoints
	 * @param agilePointDetails
     * @returns {AgilePointDetails[]|Promise<ErrorObservable>}
     * @description 
     * * this method to add new weekly status report details
    */
    public addRetroPoints(agilePointDetails: AgilePointDetails) {
        let options = new RequestOptions({
            headers: new Headers({ 'Content-Type': 'application/json;charset=UTF-8' })
        });
        return this.http.post(`${this.BASE_URL}`, JSON.stringify(agilePointDetails), options)
            .map((response: Response) => {
                let body = response.json();
                return body;
            })
            .catch((error: Response) => {
                let err = this.handleError(error);
                return Observable.throw(err);
            });
    }

    /**
     * @param error
     * @returns {ErrorObservable|any} 
     * @description 
     * * this method to handle Response error
    */
    private handleError(error: any): any {
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        let body = error.json();
        let errObj = {
            title: errMsg,
            message: body.ExceptionMessage
        }
        return errObj;
    }
}