import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PointsChanges } from 'src/app/models/interfaces';

@Component({
  selector: 'app-point-change',
  templateUrl: './point-change.component.html',
  styleUrls: ['./point-change.component.css'],
})
export class PointChangeComponent implements OnInit {
  @Input() pointChange!: PointsChanges;
  constructor() {}

  ngOnInit(): void {}
}
