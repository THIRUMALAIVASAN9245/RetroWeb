import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { environment } from '../../../environments/environment.prod';
import { RetroInfo } from '../models/retro-info';
import { RetroSearch } from '../models/retro-search';

/**
 * @name HttpRetroInfoDetailService
 * @description 
 * * this service to handle get list of RetroInfoDetail
*/
@Injectable()
export class HttpRetroInfoService {

    /**
     * @type {string}
    */
    private BASE_URL: string = environment.apiUrl + '/RetrospectiveInformation/';
    private url: string;
    /**
     * Constructor for HttpRetroInfoDetailService class
     * @param http
     * @param errorService
    */
    constructor(private http: Http) { }

    /**
     * @name getRetroInfoDetails
     * @returns {RetroInfoDetails[]|Promise<ErrorObservable>}
     * @description 
     * * this method to get list of RetroInfoDetail
    */
    public getRetroInfo(Id: Number) {
        return this.http.get(`${this.BASE_URL}${Id}`)
            .map(this.extractRetroInfo)
            .catch((error: Response) => {
                let err = this.handleError(error);
                return Observable.throw(err);
            });
    }
    /**
     * @name getRetroInfoDetails
     * @returns {RetroInfoDetails[]|Promise<ErrorObservable>}
     * @description 
     * * this method to get list of RetroInfoDetail
    */
    public getAll(retroDetails: RetroSearch) {
        this.url = this.BASE_URL;
        this.url += "?" + this.getEncodeURIData(retroDetails, "ProjectId");
        this.url += this.getEncodeURIData(retroDetails, "PageNumber");
        this.url += this.getEncodeURIData(retroDetails, "PageIndex");
        this.url = this.url.replace(/[?&]$/, "");
        return this.http.get(`${this.url}`)
            .map(this.extractRetroInfo)
            .catch((error: Response) => {
                let err = this.handleError(error);
                return Observable.throw(err);
            });
    }

    /**
     * @name addRetroInfoDetails
	 * @param retroInfoDetails
     * @returns {RetroInfoDetails[]|Promise<ErrorObservable>}
     * @description 
     * * this method to add new weekly status report details
    */
    public addRetroInfo(retroInfoDetails: RetroInfo) {
        let options = new RequestOptions({
            headers: new Headers({ 'Content-Type': 'application/json;charset=UTF-8' })
        });
        return this.http.post(`${this.BASE_URL}`, retroInfoDetails, options)
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
     * @param res
     * @returns {RetroInfoDetails[]} 
     * @description 
     * * this method to extract Response object to RetroInfoDetails
    */
    private extractRetroInfo(res: Response): RetroInfo[] {
        let body = res.json();
        var retroInfo = body || {};
        return retroInfo;
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
    private getEncodeURIData(data: any, property: string): string {
        return property + "=" + encodeURIComponent("" + data[property]) + "&";
    }
}