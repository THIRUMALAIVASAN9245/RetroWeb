import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { environment } from '../../../environments/environment.prod';
import { ProjectDetails } from '../models/project-details';

/**
 * @name HttpProjectService
 * @description 
 * * this service to handle get list of projects
*/
@Injectable()
export class HttpProjectService {

    /**
     * @type {string}
    */
    private BASE_URL: string = environment.apiUrl + '/Project/';

    /**
     * Constructor for HttpProjectService class
     * @param http
     * @param errorService
    */
    constructor(private http: Http) { }

    /**
     * @name getAllProject
     * @returns {Project[]|Promise<ErrorObservable>}
     * @description 
     * * this method to get list of projects
    */
    public getAllProject() {
        return this.http.get(`${this.BASE_URL}`)
            .map(this.extractProject)
            .catch((error: Response) => {
                let err = this.handleError(error);
                return Observable.throw(err);
            });
    }

    /**
     * @param res
     * @returns {ProjectDetails[]} 
     * @description 
     * * this method to extract Response object to ProjectDetails
    */
    private extractProject(res: Response): any {
        let body = res.json();
        var project = body || {};
        return project;
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