// angular reference
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// ngx-bootstrap reference for date picker and pagination
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';

// app component and routing config
import { AppComponent } from './app.component';
import { appRouterModule } from './app.routing';

// Common header and footer component
import { AppHeaderComponent } from './common/header/header.component';
import { AppFooterComponent } from './common/footer/footer.component';

// error handling service and component
import { PageNotFound } from './common/pageNotFound.component';

// app loading spinner
import { LoadingModule } from 'ngx-loading';

// app drag and drop
import { Ng2DragDropModule } from "ng2-drag-drop";

// component
import { RetrospectiveMeetingListComponent } from './retrospective-tool/retrospective-meeting-list/retrospective-meeting-list.component';
import { RetrospectiveMeetingDetailsComponent } from './retrospective-tool/retrospective-live/retrospective-meeting-details.component';
import { RetrospectiveMeetingGridComponent } from './retrospective-tool/retrospective-meeting-list/retrospective-grid.component';
import { SearchFilterPipe } from "./retrospective-tool/retrospective-live/search-filter.pipe";

import { HttpProjectService } from "./retrospective-tool/services/project.service";
import { HttpAgilePointService } from "./retrospective-tool/services/agile-points.service";
import { HttpImageInfoService } from "./retrospective-tool/services/image-info.service";
import { HttpRetroInfoDetailService } from "./retrospective-tool/services/retro-info-details.service";
import { HttpRetroInfoService } from "./retrospective-tool/services/retro-info.service";

import { ToasterModule, ToasterService } from 'angular2-toaster';
import { SignalRService, ConnectionStatus } from "./signal-IR/signalr.service";

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    appRouterModule,
    LoadingModule,
    ToasterModule,
    BrowserAnimationsModule,
    Ng2DragDropModule.forRoot(),
    BsDatepickerModule.forRoot(),
    PaginationModule.forRoot()
  ],
  declarations: [
    PageNotFound,
    AppComponent,
    AppHeaderComponent,
    AppFooterComponent,
    RetrospectiveMeetingListComponent,
    RetrospectiveMeetingDetailsComponent,
    RetrospectiveMeetingGridComponent,
    SearchFilterPipe
  ],
  providers: [
    HttpProjectService,
    HttpAgilePointService,
    HttpImageInfoService,
    HttpRetroInfoDetailService,
    HttpRetroInfoService,
    SignalRService,
    ToasterService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }