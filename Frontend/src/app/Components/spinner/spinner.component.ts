import { Component } from '@angular/core';
import { LoaderService } from '../../Services/Common/loader.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.scss'
})
export class SpinnerComponent {
constructor(public loader: LoaderService){}
}
