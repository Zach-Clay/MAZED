import { Component, OnInit, Input } from '@angular/core';
import { SponsorOrg, PointsChanges, User } from '../../models/interfaces';
import { PointsChangesService } from 'src/app/services/points-changes.service';
import { SponsorOrgService } from 'src/app/services/sponsor-org.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-driver-sponsor-card',
  templateUrl: './driver-sponsor-card.component.html',
  styleUrls: ['./driver-sponsor-card.component.css'],
})
export class DriverSponsorCardComponent implements OnInit {
  @Input() currentSponsor!: SponsorOrg;
  @Input() dbUser!: User;
  pointTransactions: PointsChanges[] = [];
  points!: number;
  p2drate!: string;

  constructor(
    private pointsChangesService: PointsChangesService,
    private sponsorOrgService: SponsorOrgService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.p2drate = this.currentSponsor.dollarToPoint.toFixed(2);
    this.pointsChangesService
      .getTransactions(this.dbUser.id)
      .subscribe((pointsTrans) => {
        pointsTrans.forEach((element) => {
          if (element.sponsorId == this.currentSponsor.id) {
            this.pointTransactions.push(element);
          }
        });
      });
    this.userService
      .getUserPointsBySponsor(this.dbUser.id, this.currentSponsor.id)
      .subscribe((data) => {
        this.points = Math.round(data.userPoints);
      });
  }
}
