import { Component, OnInit } from '@angular/core';
import { DetailService } from 'app/shared/services/detail.service';
import { ActivatedRoute } from '@angular/router';
import { ItemDetail } from 'app/shared/ResourceModels/DetailResponse';
import { ButtonProperties } from 'app/shared/ResourceModels/ButtonProperties';
@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DetailComponent implements OnInit {
  itemID: number;
  itemDetail: ItemDetail;
  itemImages: string[];
  imagesFound: boolean = false;
  turnLargeLoader: boolean = false;
  turnSmallLoader: boolean = false;
  detailButttons: ButtonProperties[] = new Array();
  hideItemDetailSection?: boolean = false;
  hideItemPhotoSection?: boolean = false;
  hideItemDescriptionSection?: boolean = false;
  hideItemPosterSection?: boolean = false;
  constructor(
    private detailService: DetailService,
    private route: ActivatedRoute
  ) {
    this.turnLargeLoader = true;
    this.turnSmallLoader = true;
    this.allButtons();
  }

  allButtons() {
    this.detailButttons = [
      {
        buttonId: 1,
        buttonLabel: 'Send Email',
        hasPopUp: true,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: false,
        DropDownList: null,
        popUpName: 'sendEmail'
      },
      {
        buttonId: 2,
        buttonLabel: 'All Items From This Poster',
        hasPopUp: true,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: false,
        DropDownList: null
      }
    ];
  }

  ngOnInit() {
    const result = this.route.params.subscribe(params => {
      this.itemID = +params['itemId'];
      this.getItemDetail(this.itemID);
    });
  }
  getItemDetail(id: number) {
    this.detailService.getItemDetail(id).subscribe((item: ItemDetail) => {
      this.itemDetail = item;
      this.itemImages = item.image;
      if (item.image !== null) {
        this.imagesFound = true;
      } else {
        this.imagesFound = false;
      }
      this.turnLargeLoader = false;
      this.turnSmallLoader = false;
    });
  }
  hideSection(sectionName: string) {
    switch (sectionName) {
      case 'ItemDetailSection':
        this.hideItemDetailSection = !this.hideItemDetailSection;
        break;
      case 'ItemPhotoSection':
        this.hideItemPhotoSection = !this.hideItemPhotoSection;
        break;
      case 'ItemDescriptionSection':
        this.hideItemDescriptionSection = !this.hideItemDescriptionSection;
        break;
      case 'ItemPosterSection':
        this.hideItemPosterSection = !this.hideItemPosterSection;
        break;
    }
  }
}
