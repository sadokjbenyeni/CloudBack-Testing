import { Component, OnInit, Input } from '@angular/core';
import { CardType } from '../../models/CardType';

@Component({
  selector: 'app-cardimages',
  templateUrl: './cardimages.component.html',
  styleUrls: ['./cardimages.component.css']
})
export class CardimagesComponent implements OnInit {
  @Input() cardType: CardType
  constructor() { }

  ngOnInit() {
  }

}
