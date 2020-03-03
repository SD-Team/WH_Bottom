import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root',
})
export class AlertifyService {

constructor() { }

confirm(title: string, message: string, okCallback: () => any) {
  alertify.confirm(message, function(e) {
    if (e) {
      okCallback();
    } else {}
  }).setHeader(title);
}

success(message: string) {
  alertify.success(message);
}

error(message: string) {
  alertify.error(message);
}

warring(message: string) {
  alertify.warring(message);
}

message(message: string) {
  alertify.message(message);
}

}
