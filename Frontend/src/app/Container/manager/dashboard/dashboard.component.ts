import { Component, SimpleChanges, inject } from '@angular/core';
import { ManagerStatisticsModel } from '../../../Models/ManagerStatistics.model';
import { ManagerStatisticsService } from '../../../Services/Manager/manager-statistics.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  managerStatisticsService = inject(ManagerStatisticsService);
  ErrorMessage: string = '';
  statisticsData: ManagerStatisticsModel = {
    inventoryCount: 0,
    inventoryCategoryCount: 0,
    vehicleCount: 0,
    availableVehicleCount: 0,
    vehicleTypeCount: 0,
    driverCount: 0,
    availableDriverCount: 0,
    orderCount: 0,
    pendingOrderCount: 0,
  };
  ngOnInit(changes: SimpleChanges): void {
    this.managerStatisticsService.getStatisticsData().subscribe({
      next: (response) => {
        if (response.error !== '') {
          this.ErrorMessage = response.error;
          return;
        }
        this.statisticsData =
          response.data as unknown as ManagerStatisticsModel;
      },
    });
  }
}
