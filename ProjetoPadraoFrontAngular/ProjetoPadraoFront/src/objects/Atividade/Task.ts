export class Task {
  public Id: string;

  constructor(
    public title: string,
    public points: number,
    public assignee?: string
  ) {
    this.Id = '1';
  }
}
