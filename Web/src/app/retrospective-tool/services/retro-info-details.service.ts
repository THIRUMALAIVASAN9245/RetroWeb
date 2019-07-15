import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions, ResponseContentType } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { environment } from '../../../environments/environment.prod';
import { RetroInfoDetails } from '../models/retro-info-details';

/**
 * @name HttpRetroInfoDetailService
 * @description 
 * * this service to handle get list of RetroInfoDetail
*/
@Injectable()
export class HttpRetroInfoDetailService {

    /**
     * @type {string}
    */
    private BASE_URL: string = environment.apiUrl + '/RetroInfoDetail/';

    /**
     * @type {string}
    */
    private BASE_Download_URL: string = environment.apiUrl + '/DownloadRetro/';

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
    public getRetroInfoDetails(Id: Number) {
        return this.http.get(`${this.BASE_URL}${Id}`)
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
    public addRetroInfoDetails(retroInfoDetails: RetroInfoDetails) {
        let options = new RequestOptions({
            headers: new Headers({ 'Content-Type': 'application/json;charset=UTF-8' })
        });
        return this.http.post(`${this.BASE_URL}`, JSON.stringify(retroInfoDetails), options)
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
     * @name addRetroInfoDetails
	 * @param retroInfoDetails
     * @returns {RetroInfoDetails[]|Promise<ErrorObservable>}
     * @description 
     * * this method to add new weekly status report details
    */
    public updateRetroInfoDetails(retroInfoDetails: RetroInfoDetails) {
        let options = new RequestOptions({
            headers: new Headers({ 'Content-Type': 'application/json;charset=UTF-8' })
        });
        return this.http.put(`${this.BASE_URL}`, JSON.stringify(retroInfoDetails), options)
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
 * @name deleteRetroInfoDetails
 * @param userId
 * @returns {RetroInfoDetails[]|Promise<ErrorObservable>}
 * @description 
 * * this method to add new weekly status report details
*/
    public deleteRetroInfoDetails(retroInfoId: number) {
        let url = `${this.BASE_URL}` + `${retroInfoId}`;
        let options = new RequestOptions({
            headers: new Headers({ 'Content-Type': 'application/json;charset=UTF-8' })
        });
        return this.http.delete(url, options)
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
     * @name downloadRetroInfoDetails
     * @returns {RetroInfoDetails File[]|Promise<ErrorObservable>}
     * @description 
     * * this method to get list of RetroInfoDetail
    */
    public downloadRetroInfoDetails(id: Number, imageData: any, fileName: string) {
        let options = new RequestOptions({
            headers: new Headers({ 'Content-Type': 'application/json;charset=UTF-8' }),
            responseType: ResponseContentType.Blob
        });
        let downloadRetroInfo = { Id: id, ImageData: imageData };
        return this.http.post(`${this.BASE_Download_URL}`, downloadRetroInfo, options)
            .map((response: Response) => {
                var blob = new Blob([response.blob()]);
                var filename = fileName + '.xlsx';
                if (window.navigator.msSaveOrOpenBlob) {
                    window.navigator.msSaveOrOpenBlob(blob, filename);
                } else {
                    var link = document.createElement("a");
                    if (link.download !== undefined) {
                        link.setAttribute("href", window.URL.createObjectURL(blob));
                        link.setAttribute("download", filename);
                        document.body.appendChild(link);
                        link.click();
                        document.body.removeChild(link);
                    } else {
                        console.log('CSV export only works in Chrome, Firefox, and IE.');                        
                    }
                }

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
    private extractRetroInfo(res: Response): RetroInfoDetails[] {
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
}