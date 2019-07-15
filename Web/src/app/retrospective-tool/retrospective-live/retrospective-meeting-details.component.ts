import { Component, OnInit, NgZone } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';

import { AgilePointDetails } from '../models/agile-point-details';
import { HttpAgilePointService } from '../services/agile-points.service';

import { RetroInfoDetails } from '../models/retro-info-details';
import { HttpRetroInfoDetailService } from '../services/retro-info-details.service';

import { RetroInfo } from '../models/retro-info';
import { ImageCategory } from '../models/image-category';

import { ToasterService } from 'angular2-toaster';
import { SignalRService, ConnectionStatus } from "../../signal-IR/signalr.service";

import * as html2canvas from "html2canvas";
import Swal from 'sweetalert2'

/**
 * @component
 * @description
 * This component add new weekly status report details for particular week
*/
@Component({
	selector: 'retrospective-meeting-details',
	templateUrl: './retrospective-meeting-details.component.html',
	styleUrls: ['./retrospective-meeting.css']
})

export class RetrospectiveMeetingDetailsComponent implements OnInit {

	/**
	* @type {boolean}
	*/
	public loadingAdd;

	/**
     * @type {AgilePointDetails}
    */
	public agilePointDetails: AgilePointDetails[];

	/**
     * @type {ImageCategory}
    */
	public imageCategory: ImageCategory[];

	/**
     * @type {RetroInfoDetails}
    */
	public retroInfoDetails: RetroInfoDetails;

	/**
     * @type {RetroInfo}
    */
	public retroInfo: RetroInfo;

	public RetroInfoId;
	public droppedItemsList = [];
	public addShow = false;
	public Description: string = '';
	public filterValue: string = '';
	public imageCategoryDefault = '0';
	public imageCategoryModel: ImageCategory;
	signalIRConnection = false;
	ItemsListLength: number;

	/**
     * Constructor for RetrospectiveMeetingDetailsComponent class
     * @param route
	 * @param router
	 * @param HttpAgilePointService
    */
	constructor(private route: ActivatedRoute,
		private router: Router,
		private signalrService: SignalRService,
		private zone: NgZone,
		private toasterService: ToasterService,
		private httpAgilePointService: HttpAgilePointService,
		private httpRetroInfoDetailService: HttpRetroInfoDetailService) {
		this.loadingAdd = true;
		this.router.events.subscribe((evt) => {
			if (!(evt instanceof NavigationEnd)) {
				return;
			}
			window.scrollTo(0, 100)
		});

		this.hubConnectionStart();
	}

	/**
     * @override OnInit
    */
	ngOnInit() {
		this.RetroInfoId = Number(this.route.snapshot.params['retroInfoId']);
		this.GetRetroInfoDetails();
		this.hubClientCall();
		this.imageCategoryDefault = "0";
	}

	onItemDrop(e: any) {
		if (this.retroInfo.retroinfo_status == true) {
			this.toasterService.pop("warning", "", "The retro meeting already completed not allowed to drag");
			return false;
		}
		if (this.imageCategoryModel === undefined || this.imageCategoryModel.Id === 0) {
			this.toasterService.pop("warning", "", "Please select image category.");
			return false;
		}
		if (this.signalIRConnection == true) {
			if (this.droppedItemsList.length > this.ItemsListLength) {
				this.loadingAdd = false;
				this.GetRetroInfoDetails();
				if (this.imageCategoryModel) {
					this.imageCategoryDefault = this.imageCategoryModel.Id.toString();
				}
			}
		}

		var drag = e.nativeEvent;
		var topval = drag.layerY;
		var leftval = drag.layerX;
		var width = drag.srcElement.clientWidth;
		var height = drag.srcElement.clientHeight;

		let topperval = topval / height * 100;
		topperval = Math.round(topperval);
		let leftperval = leftval / width * 100;
		leftperval = Math.round(leftperval);

		if (leftperval > 85) {
			leftperval = 85;
		}

		if (topperval > 95) {
			topperval = 95;
		}

		if (this.droppedItemsList.indexOf(e.dragData) != -1) {
			this.retroInfoDetails = new RetroInfoDetails(e.dragData.Id, this.RetroInfoId, e.dragData.Text, topperval.toString(), leftperval.toString(), this.imageCategoryModel.Id, this.imageCategoryModel.Color);
			this.httpRetroInfoDetailService.updateRetroInfoDetails(this.retroInfoDetails).subscribe(
				response => { this.onDragAndDropSuccess(); },
				error => { this.loadingAdd = false; console.log(error); }
			);

		} else {
			this.retroInfoDetails = new RetroInfoDetails(0, this.RetroInfoId, e.dragData.Text, topperval.toString(), leftperval.toString(), this.imageCategoryModel.Id, this.imageCategoryModel.Color);
			this.httpRetroInfoDetailService.addRetroInfoDetails(this.retroInfoDetails).subscribe(
				response => { this.addRetroObject(response); this.onDragAndDropSuccess() },
				error => { this.loadingAdd = false; console.log(error); }
			);
		}
	}

	onDragAndDropSuccess() {
		this.signalIRConnection = true;
		this.ItemsListLength = this.droppedItemsList.length;
		this.loadingAdd = false;
	}

	onDropSelected(e: any) {
		if (this.retroInfo.retroinfo_status == true) {
			this.toasterService.pop("warning", "", "The retro meeting already completed not allowed to drag");
			return false;
		}

		if (e.dragData) {
			this.httpRetroInfoDetailService.deleteRetroInfoDetails(e.dragData.Id).subscribe(
				response => {
					debugger;
					this.droppedItemsList = this.droppedItemsList.filter(function (el) {
						return el.Id !== response.Id;
					});
					this.onDragAndDropSuccess()
				},
				error => { this.loadingAdd = false; console.log(error); }
			);
		}
	}

	addSave() {
		this.loadingAdd = true;
		let agilePointDetails = new AgilePointDetails(0, this.Description, this.Description);
		this.httpAgilePointService.addRetroPoints(agilePointDetails).subscribe(
			response => { this.loadingAdd = false; this.addCancel(); },
			error => { this.loadingAdd = false; console.log(error); }
		);
	}

	onChangeImageCategory(selectedCategory) {
		if (selectedCategory.target.selectedIndex > 0) {
			this.imageCategoryModel = this.imageCategory[selectedCategory.target.selectedIndex - 1];
			this.imageCategoryDefault = this.imageCategoryModel.Id.toString();
		}
		else {
			this.imageCategoryDefault = "0";
			this.imageCategoryModel = new ImageCategory(0, "", "", "");
		}
	}

	addCancel() {
		this.addShow = false;
		this.Description = '';
	}

	addNew() {
		this.addShow = true;
	}

	getBackgroundImageStyles() {
		let imageStyle = {
			'background-image': this.retroInfo ? 'url(' + this.retroInfo.retroinfo_imagepath + ')' : ''
		};
		return imageStyle;
	}

	GetRetroInfoDetails() {
		this.httpRetroInfoDetailService.getRetroInfoDetails(this.RetroInfoId).subscribe(
			response => {
				this.droppedItemsList = response.RetroInfoDetails;
				this.retroInfo = response.RetroInfoModel;
				this.agilePointDetails = response.AgilePointDetails;
				this.imageCategory = response.ImageCategory;
				this.ItemsListLength = this.droppedItemsList.length;
				this.loadingAdd = false;
			},
			error => { this.loadingAdd = false; console.log(error); }
		);
	}

	addRetroObject(response: any) {
		let ifExists = this.droppedItemsList.filter(a => a.Id === response.Id);
		if (ifExists.length == 0) {
			this.droppedItemsList.push(response);
		}
	}

	hubConnectionStart() {
		let subscribeGroupId = this.route.snapshot.params['retroInfoId'];

		this.signalrService.start("retrospectivehub", true, subscribeGroupId).subscribe(
			(connectionStatus: ConnectionStatus) => {
				console.log(`[signalr] start() - done - ${connectionStatus}`);
			},
			(error: any) => {
				console.log(`[signalr] start() - fail - ${error}`);
			});

		this.signalrService.error.subscribe((error: any) => {
			console.log(`[signalr] error - ${error}`);
		});
	}

	hubClientCall() {
		this.signalrService.addEventListener("addItem").subscribe(
			(response: any) => {
				this.zone.run(() => {
					this.addRetroObject(response);
				});
			});

		this.signalrService.addEventListener("updateItem").subscribe(
			(response: any) => {
				this.zone.run(() => {
					let filter = this.droppedItemsList.filter(a => a.Id === response.Id)[0];
					if (filter) {
						this.droppedItemsList[this.droppedItemsList.indexOf(filter)] = response;
					}
				});
			});

		this.signalrService.addEventListener("deleteItem").subscribe(
			(response: any) => {
				this.zone.run(() => {
					this.droppedItemsList = this.droppedItemsList.filter(function (el) {
						return el.Id !== response.Id;
					});
				});
			});

		this.signalrService.addEventListener("addRetroItem").subscribe(
			(response: any) => {
				this.zone.run(() => {
					this.agilePointDetails.push(response);
					this.toasterService.pop("success", "", "New retrospective point added successfully.");
				});
			});

		this.signalrService.addEventListener("downloadCompleted").subscribe(
			(response: any) => {
				this.zone.run(() => {
					this.retroInfo.retroinfo_status = true;
				});
			});
	}

	public DownloadRetroMeeting() {
		this.loadingAdd = true;
		// let isChrome = /\/Firefox|Chrome\//i.test(window.navigator.userAgent);
		var isFirefox = navigator.userAgent.search("Firefox");
		var isChrome = navigator.userAgent.search("Chrome");
		if (isFirefox > -1 || isChrome > -1) {
			html2canvas(document.querySelector('#imageDownloadView')).then(canvas => {
				var imgageData = canvas.toDataURL("image/jpeg");
				this.httpRetroInfoDetailService.downloadRetroInfoDetails(this.RetroInfoId, imgageData, this.retroInfo.retroinfo_name).subscribe(
					response => { this.loadingAdd = false; this.retroInfo.retroinfo_status = true; console.log(response); },
					error => { this.loadingAdd = false; console.log(error); }
				);
			});
		}
		else {
			this.httpRetroInfoDetailService.downloadRetroInfoDetails(this.RetroInfoId, null, this.retroInfo.retroinfo_name).subscribe(
				response => { this.loadingAdd = false; this.retroInfo.retroinfo_status = true; console.log(response); },
				error => { this.loadingAdd = false; console.log(error); }
			);
		}
	}
}
