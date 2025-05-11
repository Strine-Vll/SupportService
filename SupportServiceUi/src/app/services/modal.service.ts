import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class ModalService {

  private modalsVisibility = new Map<string, BehaviorSubject<boolean>>();
  
  private getModalSubject(key: string): BehaviorSubject<boolean> {
    if (!this.modalsVisibility.has(key)) {
      this.modalsVisibility.set(key, new BehaviorSubject<boolean>(false));
    }
    return this.modalsVisibility.get(key)!;
  }

  open(key: string): void {
    this.getModalSubject(key).next(true);
  }
  
  close(key: string): void {
    this.getModalSubject(key).next(false);
  }
  
  isVisible$(key: string) {
    return this.getModalSubject(key).asObservable();
  }
}
