import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Config } from '../config';

@Injectable({ providedIn: 'root' })
export class PaymentService {

  constructor(private http: HttpClient) { }

  createPaymentIntent(amount: number, event_id: number) {
    return this.http.post<any>(Config.api +
      '/Payments/CreatePaymentIntent',
      {
        event_id,
        amount
      },
      Config.http_options()
    );
  }
}