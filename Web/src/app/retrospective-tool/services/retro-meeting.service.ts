import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { environment } from '../../../environments/environment.prod';

/**
 * @name HttpRetroMeetingService
 * @description 
 * * this service to handle Http Retro Meeting Service
*/
@Injectable()
export class HttpRetroMeetingService {

    /**
     * @type {string}
    */
    private BASE_URL: string = environment.apiUrl + '/Project/';

    /**
   * @type {object}
   */
    public retroDetails: any;

    /**
     * Constructor for HttpProjectService class
     * @param http
    */
    constructor(private http: Http) { }

    /**
     * @param any
     * @returns {any} 
     * @description 
     * * this method to get retro object
    */
    public getRetroObject(): any {
        return this.retroDetails;
    }

    /**
   * @param any
   * @returns {any} 
   * @description 
   * * this method to set retro object
  */
  public setRetroObject(retroDetails: any): any {
        return this.retroDetails = retroDetails;
    }
}