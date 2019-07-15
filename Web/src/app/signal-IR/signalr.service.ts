import { Injectable, Inject } from "@angular/core";
import { Subject, Observable } from "rxjs";

import { environment } from '../../environments/environment.prod';
declare var $: any;

export enum ConnectionStatus {
    Connected = 1,
    Disconnected = 2,
    Error = 3
}

@Injectable()
export class SignalRService {
    private hubConnection: any;
    private hubProxy: any;
    error: Observable<any>;

    /**
     * @type {string}
    */
    private BASE_URL: string = environment.signalIRUri;

    constructor() {
        if ($ === undefined || $.hubConnection === undefined) {
            throw new Error("The '$' or the '$.hubConnection' are not defined...");
        }
        this.hubConnection = $.hubConnection();
        this.hubConnection.url = this.BASE_URL;
    }

    start(hubName: string, debug: boolean = false, subscribeGroupId: string): Observable<ConnectionStatus> {
        this.hubConnection.logging = debug;
        this.hubProxy = this.hubConnection.createHubProxy(hubName);

        let errorSubject = new Subject<any>();
        this.error = errorSubject.asObservable();
        this.hubConnection.error((error: any) => errorSubject.next(error));

        let subject = new Subject<ConnectionStatus>();
        let observer = subject.asObservable();

        this.hubConnection.start()
            .done(() => {
                this.hubProxy.invoke("Subscribe", subscribeGroupId);
                this.hubProxy.invoke("Subscribe", "AgilePoint");
                subject.next(ConnectionStatus.Connected)
            })
            .fail((error: any) => subject.error(error));
        return observer;
    }

    addEventListener(eventName: string): Observable<any> {
        let subject = new Subject<any>();
        let observer = subject.asObservable();
        this.hubProxy.on(eventName, (data: any) => subject.next(data));
        return observer;
    }

    invoke(eventName: string, data: any): void {
        this.hubProxy.invoke(eventName, data);
    }
}