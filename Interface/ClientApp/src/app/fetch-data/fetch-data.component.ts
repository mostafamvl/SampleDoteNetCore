import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public requestObj: Request[];
  public to: string = "" ;
  public from: string = "";
  public http: HttpClient;
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    http.get<any>("https://localhost:44308/api/Request").subscribe(result => {
      this.requestObj = result;
    }, error => console.error(error));
  }


  onFromSearchChange(fromFilter: string) {
    this.http.get<any>("https://localhost:44308/api/Request?dtTo=" + this.to + "&dtFrom=" + fromFilter + "").subscribe(result => {
      this.requestObj = result;
    });
    this.from = fromFilter;
  }

  onToSearchChange(toFilter: string) {
    this.http.get<any>("https://localhost:44308/api/Request?dtTo=" + toFilter + "&dtFrom=" + this.from + "").subscribe(result => {
      this.requestObj = result;
    });
    this.to = toFilter;
  }

}
interface Request {
  RequestId: number;
  RequestDate: DatePipe;
  MobileNumber: number;
}
