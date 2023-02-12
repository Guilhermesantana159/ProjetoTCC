import { EOperadorFilter } from 'src/enums/EOperadorFilter'
import {TypeFilter} from '../../enums/TypeFilter'

export interface Filter{
   Value: string,
   Type: TypeFilter,
   Field: string,
   EOperadorFilter: EOperadorFilter
}
