export class RetroInfoModel {
    /**
     * Constructor for RetroInfoDetails class
     * @param retroinfo_id
     * @param retroinfo_name
     * @param retroinfo_projectinfo_id
     * @param retroinfo_sprint
     * @param retroinfo_date
    */
    constructor(
        public retroinfo_id: number,
        public retroinfo_name: string,
        public retroinfo_projectinfo_id: number,
        public retroinfo_sprint: string,
        public retroinfo_date: string,
        public retroinfo_imagepath: string,
    ) { }
} 