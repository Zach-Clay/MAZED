import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PointsChanges, User } from 'src/app/models/interfaces';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-point-change',
  templateUrl: './point-change.component.html',
  styleUrls: ['./point-change.component.css'],
})
export class PointChangeComponent implements OnInit {
  @Input() pointChange!: PointsChanges;
  @Input() isReport: boolean = false;
  user!: User;
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService
      .getUserById(this.pointChange.userId)
      .subscribe((u) => (this.user = u));
  }
}
