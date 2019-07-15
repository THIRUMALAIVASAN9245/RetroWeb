import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { HttpRetroInfoService } from '../services/retro-info.service';
import { HttpProjectService } from '../services/project.service';
import { RetroInfo } from '../models/retro-info';
import { HttpImageInfoService } from '../services/image-info.service';
import { ImageInfoDetails } from '../models/image-info-details';
import { ProjectDetails } from '../models/project-details';
import { RetroSearch } from '../models/retro-search';
/**
 * @component
 * @description
 * This component to get all retrospective meeting list details with filter 
*/
@Component({
  selector: 'retrospective-meeting-list',
  templateUrl: './retrospective-meeting-list.component.html',
  styleUrls: ['./retrospective-meeting-list.css']
})

export class RetrospectiveMeetingListComponent implements OnInit {
  /**
  * @type {boolean}
  */
  public loading;
  public itemsPath;
  public items: any;
  retroForm: FormGroup;
  public imageIndex = 0;
  public imageList: ImageInfoDetails[];
  public retroInfoData: RetroInfo[];
  public projects: ProjectDetails[];
  public totalItems: number;
  public currentPage: number;
  public searchProjectId: number;
  /**
    * Constructor for RetrospectiveMeetingListComponent class
  */
  constructor(private router: Router,
    private httpRetroInfoService: HttpRetroInfoService,
    private httpProjectService: HttpProjectService,
    private httpImageInfoService: HttpImageInfoService) {
    this.loading = true;
    this.searchProjectId = 0;
    this.currentPage = 1;
  }

  /**
    * @override OnInit
  */
  ngOnInit() {
    this.retroForm = new FormGroup({
      RetroName: new FormControl(null, [Validators.required]),
      Sprint: new FormControl(null, [Validators.required]),
      Project: new FormControl("", [Validators.required])
    });
    let retroDetails = {
      ProjectId: 0,
      PageNumber: 1,
      PageIndex: 10
    }
    this.getAllDetails(retroDetails);

    this.httpImageInfoService.getImageInfo().subscribe(
      response => { this.imageList = response; this.items = this.imageList[0]; this.loading = false; },
      error => { this.loading = false; console.log(error); }
    );

    this.httpProjectService.getAllProject().subscribe(
      response => { this.projects = response; this.loading = false; },
      error => { this.loading = false; console.log(error); }
    );
  }

  onSubmit() {
    var retroObj = {
      "retroinfo_id": 0,
      "retroinfo_status": false,
      "retroinfo_name": this.retroForm.value.RetroName,
      "retroinfo_projectinfo_id": this.retroForm.value.Project,
      "retroinfo_imagepath": this.items.Path,
      "retroinfo_imageinfo_id": this.items.Id,
      "retroinfo_sprint": this.retroForm.value.Sprint,
      "retroinfo_date": null,
      "retroinfo_projectinfo_name": null
    };
    this.httpRetroInfoService.addRetroInfo(retroObj).subscribe((data) => {
      this.router.navigate(["retrospective-meeting", data]);
    }, (err) => {
      console.log("error");
    });
  }

  selectedImage(item: any): void {
    this.items = item;
  }

  dblclick(path: string): void {
    this.itemsPath = path;
    document.getElementById("modal01").style.display = "block";
  }

  public pageChanged(event: any): void {
    let retroDetails = {
      ProjectId: this.searchProjectId,
      PageNumber: event.page,
      PageIndex: event.itemsPerPage
    }
    this.getAllDetails(retroDetails);
  }

  private getAllDetails(retroDetails: RetroSearch): void {
    this.httpRetroInfoService.getAll(retroDetails).subscribe(
      response => {
        this.retroInfoData = response.RetroInfoModel;
        this.totalItems = response.TotalRecords;
        this.loading = false;
      },
      error => { this.loading = false; console.log(error); }
    );
  }

  public SearchRetro(id: number): void {
    let retroDetails = {
      ProjectId: +id,
      PageNumber: 1,
      PageIndex: 10
    }
    this.getAllDetails(retroDetails);
  }
}