import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/Rx';

import { environment } from '../../../environments/environment.prod';
import { ImageInfoDetails } from '../models/image-info-details';

/**
 * @name HttpImageInfoService
 * @description 
 * * this service to handle get list of ImageInfo
*/
@Injectable()
export class HttpImageInfoService {

    /**
     * @type {string}
    */
    private BASE_URL: string = environment.apiUrl + '/ImageInfo/';

    /**
     * Constructor for HttpImageInfoService class
     * @param http
     * @param errorService
    */
    constructor(private http: Http) { }

    /**
     * @name getImageInfo
     * @returns {ImageInfo[]|Promise<ErrorObservable>}
     * @description 
     * * this method to get list of ImageInfo
    */
    public getImageInfo() {
        return this.http.get(`${this.BASE_URL}`)
            .map(this.extractImageInfo)
            .catch((error: Response) => {
                let err = this.handleError(error);
                return Observable.throw(err);
            });
    }

    /**
     * @param res
     * @returns {ImageInfoDetails[]} 
     * @description 
     * * this method to extract Response object to ImageInfo
    */
    private extractImageInfo(res: Response): ImageInfoDetails[] {
        let body = res.json();
        var imageInfo = body || {};
        return imageInfo;
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