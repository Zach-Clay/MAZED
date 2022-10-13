import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/models/interfaces';

@Component({
  selector: 'app-sponsored-driver',
  templateUrl: './sponsored-driver.component.html',
  styleUrls: ['./sponsored-driver.component.css'],
})
export class SponsoredDriverComponent implements OnInit {
  @Input() sponsoredDriver!: User;

  constructor() {}

  ngOnInit(): void {}
}
